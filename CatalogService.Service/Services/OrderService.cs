using CatalogService.Core.Interfaces.Repositories;
using CatalogService.Core.Interfaces.Services;
using CatalogService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CatalogService.Core.Entities;
using System.Threading.Tasks;
using CatalogService.Core.Enums;

namespace CatalogService.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<GeneralResponse<List<ProductResponse>>> GetCountOfProductsByStatus(int customerId)
        {
            var respnose = new GeneralResponse<List<ProductResponse>>();

            var isValid =  customerId > 0;
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            var product = _orderRepository.GetCountOfProductsByStatus();

            if (product == null)
            {
                respnose.Message = "Not Found";
                return respnose;
            }

            List<ProductResponse> Temp = new List<ProductResponse>();
            foreach (var item in product)
            {
                var productResponse = new ProductResponse
                {
                    Code = item.Code,
                    Name = item.Name,
                    Category = item.Name,
                    Status = item.Status,
                    Quantity = item.Quantity,
                    SoldQuantity = item.SoldQuantity,
                    DamagedQuantity = item.DamagedQuantity
                };
                Temp.Add(productResponse);
            }


            respnose = new GeneralResponse<List<ProductResponse>> ()
            {
                Success = true,
                Data = Temp
            };

            return respnose;
        }

        public async Task<GeneralResponse<string>> ChangeProductStatus(ProductRequest productRequest)
        {
            var respnose = new GeneralResponse<string>();

            var isValid = productRequest.CustomerId > 0;
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            var product = _orderRepository.GetProductById(productRequest.ProductId);

            if (product == null)
            {
                respnose.Message = "Not Found";
                return respnose;
            }

            switch (productRequest.Status)
            {
                case ProductStatusType.InStock:
                    product.Status = productRequest.Status;
                    product.Quantity += productRequest.QTY;
                    break;
                case ProductStatusType.Sold:
                    product.Status = productRequest.Status;
                    product.Quantity -= productRequest.QTY;
                    product.SoldQuantity += productRequest.QTY;
                    break;
                case ProductStatusType.Damaged:
                    product.Status = productRequest.Status;
                    product.Quantity -= productRequest.QTY;
                    product.DamagedQuantity += productRequest.QTY;
                    break;
                default:
                    product.Status = productRequest.Status;
                    break;
            }

            var result = _orderRepository.ChangeProductStatus(product);

            respnose = new GeneralResponse<String>()
            {
                Success = true,
                Data = "Saved Successfully .."
            };

            return respnose;
        }

        public async Task<GeneralResponse<OrderResponse>> CreateNewOrder(OrderRequest request)
        {
            var respnose = new GeneralResponse<OrderResponse>();

            var isValid = request.OrderDetailsList?.Count > 0 &&
                request.OrderDetailsList.All(a => a?.ProductId > 0 &&
                                                  a?.Quantity > 0 &&
                                                  a.Quantity <= _orderRepository.GetProductById(a.ProductId).Quantity);
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }
             
            
            var orderModel = new SalesOrder
            {
                CustomerId = request.CustomerId,
                OrderStatus = Core.Enums.OrderStatusType.New,
                OrderCreateDate = DateTime.Now,
                SalesOrderDetails = request.OrderDetailsList.Select(a => new SalesOrderDetails
                {
                    ProductId = a.ProductId,
                    Quantity = a.Quantity
                }).ToList()
            };

            var order = _orderRepository.CreateNewOrder(orderModel);

            if (order == null)
            {
                respnose.Message = "Save Error";
                return respnose;
            }

            var orderResponse = new OrderResponse
            {
                OrderId = order.Id,
                OrderStatus = order.OrderStatus,
                OrderCreateDate = order.OrderCreateDate,
                OrderDetailsList = order.SalesOrderDetails.Select(a => new OrderDetailsResponse
                {
                    ProductId = a.Product.Id,
                    ProductName = a.Product.Name,
                    Quantity = a.Quantity
                }).ToList()
            };

            respnose = new GeneralResponse<OrderResponse>()
            {
                Success = true,
                Data = orderResponse
            };

            return respnose;
        }

        public async Task<GeneralResponse<OrderResponse>> GetOrderDetails(int orderId, int customerId)
        {
            var respnose = new GeneralResponse<OrderResponse>();

            var isValid = orderId > 0 && customerId > 0;
            if (!isValid)
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            var order = _orderRepository.GetOrderDetails(orderId, customerId);

            if (order == null)
            {
                respnose.Message = "Not Found";
                return respnose;
            }

            var orderResponse = new OrderResponse
            {
                OrderId = order.Id,
                OrderStatus = order.OrderStatus,
                OrderCreateDate = order.OrderCreateDate,
                OrderDetailsList = order.SalesOrderDetails.Select(a => new OrderDetailsResponse
                {
                    ProductId = a.Product.Id,
                    ProductName = a.Product.Name,
                    Quantity = a.Quantity
                }).ToList()
            };

            respnose = new GeneralResponse<OrderResponse>()
            {
                Success = true,
                Data = orderResponse
            };

            return respnose;
        }
    }
}
