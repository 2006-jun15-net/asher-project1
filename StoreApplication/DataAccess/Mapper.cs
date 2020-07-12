using AutoMapper;
using System.Linq;

namespace DataAccess

{
    public static class Mapper
    {
        public static StoreApplication.Library.Models.Customer MapCustomerEntity(Entities.Customer customer)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Customer, StoreApplication.Library.Models.Customer>()
                  .ForMember(dest => dest.Id, act => act.MapFrom(src => src.CustomerId));
            });
            IMapper mapper = config.CreateMapper();

            var customerModel = mapper.Map<StoreApplication.Library.Models.Customer>(customer);
            customerModel.Orders = customer.OrderHistory.Select(MapOrderHistoryEntity).ToList();

            return customerModel;
        }

        public static Entities.Customer MapCustomerDTO(StoreApplication.Library.Models.Customer customer)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StoreApplication.Library.Models.Customer, Entities.Customer>()
                .ForMember(dest => dest.CustomerId, act => act.MapFrom(src => src.Id));
            });
            IMapper mapper = config.CreateMapper();

            var customerEntity = mapper.Map<Entities.Customer>(customer);
            customerEntity.OrderHistory = customer.Orders.Select(MapOrderHistoryDTO).ToList();

            return customerEntity;
        }


        public static StoreApplication.Library.Models.Location MapLocationEntities(Entities.Location location)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Location, StoreApplication.Library.Models.Location>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.LocationId));
            });
            IMapper mapper = config.CreateMapper();

            var locationEntity = mapper.Map<StoreApplication.Library.Models.Location>(location);
            locationEntity.Inventories = location.Inventory.Select(MapInventoryEntities).ToList();

            return locationEntity;
        }

        public static Entities.Location MapLocationDTO(StoreApplication.Library.Models.Location location)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StoreApplication.Library.Models.Location, Entities.Location>()
                .ForMember(dest => dest.LocationId, act => act.MapFrom(src => src.Id));
            });
            IMapper mapper = config.CreateMapper();

            var locationEntity = mapper.Map<Entities.Location>(location);
            locationEntity.Inventory = location.Inventories.Select(MapInventoryDTO).ToList();

            return locationEntity;
        }
        public static StoreApplication.Library.Models.Order MapOrdersEntity(Entities.Orders orders)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Orders, StoreApplication.Library.Models.Order>()
                  .ForMember(dest => dest.Id, act => act.MapFrom(src => src.OrderId))
                  .ForMember(dest => dest.ProductName, act => act.MapFrom(src => src.Product.Name));
            });
            IMapper mapper = config.CreateMapper();

            return mapper.Map<StoreApplication.Library.Models.Order>(orders);
        }

        public static Entities.Orders MapOrdersDTO(StoreApplication.Library.Models.Order order)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StoreApplication.Library.Models.Order, Entities.Orders>()
                  .ForMember(dest => dest.OrderId, act => act.MapFrom(src => src.Id));
            });
            IMapper mapper = config.CreateMapper();

            return mapper.Map<Entities.Orders>(order);
        }



        public static StoreApplication.Library.Models.OrderHistory MapOrderHistoryEntity(Entities.OrderHistory orderHistory)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.OrderHistory, StoreApplication.Library.Models.OrderHistory>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.OrderHistoryId));
            });
            IMapper mapper = config.CreateMapper();

            return mapper.Map<StoreApplication.Library.Models.OrderHistory>(orderHistory);
        }

        public static Entities.OrderHistory MapOrderHistoryDTO(StoreApplication.Library.Models.OrderHistory orderHistory)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StoreApplication.Library.Models.OrderHistory, Entities.OrderHistory>()
                .ForMember(dest => dest.OrderHistoryId, act => act.MapFrom(src => src.Id));
            });
            IMapper mapper = config.CreateMapper();

            return mapper.Map<Entities.OrderHistory>(orderHistory);
        }



        public static StoreApplication.Library.Models.Product MapProductEntity(Entities.Product product)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Product, StoreApplication.Library.Models.Product>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.ProductId));
            });
            IMapper mapper = config.CreateMapper();

            return mapper.Map<StoreApplication.Library.Models.Product>(product);
        }

        public static Entities.Product MapProductDTO(StoreApplication.Library.Models.Product product)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StoreApplication.Library.Models.Product, Entities.Product>()
                .ForMember(dest => dest.ProductId, act => act.MapFrom(src => src.Id));
            });
            IMapper mapper = config.CreateMapper();

            return mapper.Map<Entities.Product>(product);
        }


        public static StoreApplication.Library.Models.Inventory MapInventoryEntities(Entities.Inventory inventory)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Inventory, StoreApplication.Library.Models.Inventory>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.InventoryId));
            });
            IMapper mapper = config.CreateMapper();

            return mapper.Map<StoreApplication.Library.Models.Inventory>(inventory);
        }

        public static Entities.Inventory MapInventoryDTO(StoreApplication.Library.Models.Inventory inventory)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StoreApplication.Library.Models.Inventory, Entities.Inventory>()
                .ForMember(dest => dest.InventoryId, act => act.MapFrom(src => src.Id));
            });
            IMapper mapper = config.CreateMapper();

            return mapper.Map<Entities.Inventory>(inventory);
        }
    }
}
