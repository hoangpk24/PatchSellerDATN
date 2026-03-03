using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;

namespace PatchSeller.API.Utilities
{
    public class OrderManagerService
    {
        private readonly OrderRepository _orderRepository;
        private readonly OrderDetailRepository _orderDetailRepository;

        public OrderManagerService(OrderRepository orderRepository, OrderDetailRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderItemRepository;
        }

        public async Task CancelExpiredOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);
            if (order != null && order.Status == Constant.OrderStatus.OrderWaitingForPayment && order.PaymentExpiration < DateTime.Now)
            {
                order.Status = Constant.OrderStatus.OrderCanceled;
                await _orderRepository.Update(order);                

            }
        }



    }
}
