using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with {request.ProductName} is not found."));
            }

            var mapCoupon = _mapper.Map<CouponModel>(coupon);
            return mapCoupon;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var mapCoupon = _mapper.Map<Coupon>(request.Coupon);

            await _discountRepository.CreateCoupon(mapCoupon);

            var coupon = _mapper.Map<CouponModel>(mapCoupon);
            return coupon;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var mapCoupon = _mapper.Map<Coupon>(request.Coupon);

            await _discountRepository.UpdateCoupon(mapCoupon);

            var coupon = _mapper.Map<CouponModel>(mapCoupon);
            return coupon;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _discountRepository.DeleteCoupon(request.ProductName);

            var response = new DeleteDiscountResponse()
            {
                Success = deleted
            };

            return response;
        }
    }
}
