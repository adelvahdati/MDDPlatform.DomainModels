using MDDPlatform.DomainModels.Core.Entities;
using MDDPlatform.DomainModels.Services.Repositories;

namespace MDDPlatform.DomainModels.Infrastructure.Data.Seeders;
public class DataSeeder : IDataSeeder
{
    private readonly IDomainModelRepository _domainModelRepository;

    public DataSeeder(IDomainModelRepository domainModelRepository)
    {
        _domainModelRepository = domainModelRepository;
    }

    public async Task SeedCartModelAsync(Guid modelId)
    {
        string Command = "Command.Concept";
        string Event = "Event.Concept";
        string DomainConcept = "DomainConcept.Concept";
        string publishFact = "publishFact";
        string callForAction = "callForAction";
        string isResponsibleFor = "isResponsibleFor";
        string isInterestedIn = "isInterestedIn";

        DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(modelId);
        domainModel.ClearInstances();

        ///////////////////////////// Cart Microservice/////////////////////////////////////////
        domainModel.CreateDomainObject(Command,"AddProductToCart");
        domainModel.CreateDomainObject(Event,"ProductAddedToCart");
        domainModel.SetRelationTargetInstance(Command,"AddProductToCart",publishFact,Event,"ProductAddedToCart");

        domainModel.CreateDomainObject(Command,"DeleteProductFromCart");
        domainModel.CreateDomainObject(Event,"ProductDeletedFromCart");
        domainModel.SetRelationTargetInstance(Command,"DeleteProductFromCart",publishFact,Event,"ProductDeletedFromCart");

        domainModel.CreateDomainObject(Event,"OrderCanceled");
        domainModel.CreateDomainObject(Command,"ReleaseCartItems");
        domainModel.SetRelationTargetInstance(Event,"OrderCanceled",callForAction,Command,"ReleaseCartItems");

        domainModel.CreateDomainObject(Event,"OrderApproved");
        domainModel.CreateDomainObject(Command,"DecreaseProductQuantity");
        domainModel.SetRelationTargetInstance(Event,"OrderApproved",callForAction,Command,"DecreaseProductQuantity");

        domainModel.CreateDomainObject(Event,"OrderCompleted");
        domainModel.CreateDomainObject(Command,"ClearCart");
        domainModel.SetRelationTargetInstance(Event,"OrderCompleted",callForAction,Command,"ClearCart");

        domainModel.CreateDomainObject(Event,"ProductUpdated");
        domainModel.CreateDomainObject(Command,"UpdateCart");
        domainModel.SetRelationTargetInstance(Event,"ProductUpdated",callForAction,Command,"UpdateCart");

        domainModel.CreateDomainObject(Event,"ProductDeleted");
        domainModel.CreateDomainObject(Command,"UpdateCart");
        domainModel.SetRelationTargetInstance(Event,"ProductDeleted",callForAction,Command,"UpdateCart");

        domainModel.CreateDomainObject(Event,"ProductCreated");
        domainModel.CreateDomainObject(Command,"IncreaseProductQuantity");
        domainModel.SetRelationTargetInstance(Event,"ProductCreated",callForAction,Command,"IncreaseProductQuantity");

        domainModel.CreateDomainObject(Event,"CustomerCreated");
        domainModel.CreateDomainObject(Command,"CreateCart");
        domainModel.SetRelationTargetInstance(Event,"CustomerCreated",callForAction,Command,"CreateCart");

        domainModel.CreateDomainObject(DomainConcept,"Cart");
        domainModel.SetRelationTargetInstance(DomainConcept,"Cart",isResponsibleFor,Command,"AddProductToCart, DeleteProductFromCart, ReleaseCartItems, ClearCart, UpdateCart,CreateCart");
        domainModel.SetRelationTargetInstance(DomainConcept,"Cart",isInterestedIn,Event,"OrderCanceled, OrderCompleted, ProductUpdated, ProductDeleted, CustomerCreated");

        domainModel.CreateDomainObject(DomainConcept,"Product");
        domainModel.SetRelationTargetInstance(DomainConcept,"Product",isInterestedIn,Event,"OrderApproved, ProductCreated");

        domainModel.CreateDomainObject(DomainConcept,"CartItem");

        await _domainModelRepository.UpdateDomainModelAsync(domainModel);
    }

    public async Task SeedOrderModelAsync(Guid modelId)
    {
        string Command = "Command.Concept";
        string Event = "Event.Concept";
        string DomainConcept = "DomainConcept.Concept";
        string publishFact = "publishFact";
        string callForAction = "callForAction";
        string isResponsibleFor = "isResponsibleFor";
        string isInterestedIn = "isInterestedIn";

        DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(modelId);
        domainModel.ClearInstances();

        ///////////////////////////////// Order Microservice///////////////////////////////////////
        domainModel.CreateDomainObject(Command,"CreateOrder");
        domainModel.CreateDomainObject(Event,"OrderCreated");
        domainModel.CreateDomainObject(Event,"CreateOrderRejected");
        domainModel.SetRelationTargetInstance(Command,"CreateOrder",publishFact,Event,"OrderCreated, CreateOrderRejected");


        domainModel.CreateDomainObject(Command,"ApproveOrder");
        domainModel.CreateDomainObject(Event,"OrderApproved");
        domainModel.CreateDomainObject(Event,"ApproveOrderRejected");
        domainModel.SetRelationTargetInstance(Command,"ApproveOrder",publishFact,Event,"OrderApproved, ApproveOrderRejected");


        domainModel.CreateDomainObject(Command,"CancelOrder");
        domainModel.CreateDomainObject(Event,"OrderCanceled");
        domainModel.CreateDomainObject(Event,"CancelOrderRejected");
        domainModel.SetRelationTargetInstance(Command,"CancelOrder",publishFact,Event,"OrderCanceled, CancelOrderRejected");

        domainModel.CreateDomainObject(Command,"CompleteOrder");
        domainModel.CreateDomainObject(Event,"OrderCompleted");
        domainModel.CreateDomainObject(Event,"CompleteOrderRejected");
        domainModel.SetRelationTargetInstance(Command,"CompleteOrder",publishFact,Event,"OrderCompleted, CompleteOrderRejected");        

        domainModel.CreateDomainObject(Event,"CustomerCreated");
        domainModel.CreateDomainObject(Command,"SaveCustomer");
        domainModel.SetRelationTargetInstance(Event,"CustomerCreated",callForAction,Command,"SaveCustomer");
        
        domainModel.CreateDomainObject(DomainConcept,"Order");
        domainModel.SetRelationTargetInstance(DomainConcept,"Order",isResponsibleFor,Command,"CreateOrder, ApproveOrder, CancelOrder, CompleteOrder");

        domainModel.CreateDomainObject(DomainConcept,"Customer");
        domainModel.SetRelationTargetInstance(DomainConcept,"Customer",isInterestedIn,Event,"CustomerCreated");
        domainModel.SetRelationTargetInstance(DomainConcept,"Customer",isResponsibleFor,Command,"SaveCustomer");

        domainModel.CreateDomainObject(DomainConcept,"OrderItem");

        await _domainModelRepository.UpdateDomainModelAsync(domainModel);
    }

    public async Task SeedProductModelAsync(Guid modelId)
    {
        string Command = "Command.Concept";
        string Event = "Event.Concept";
        string DomainConcept = "DomainConcept.Concept";
        string publishFact = "publishFact";
        string callForAction = "callForAction";
        string isResponsibleFor = "isResponsibleFor";
        string isInterestedIn = "isInterestedIn";

        DomainModel domainModel = await _domainModelRepository.GetDomainModelAsync(modelId);
        domainModel.ClearInstances();
        
        /////////////////////////////////////// Product Microservice //////////////////////////////
        domainModel.CreateDomainObject(Command,"CreateProduct");
        domainModel.CreateDomainObject(Event,"ProductCreated");
        domainModel.CreateDomainObject(Event,"CreateProductRejected");
        domainModel.SetRelationTargetInstance(Command,"CreateProduct",publishFact,Event,"ProductCreated, CreateProductRejected");

        domainModel.CreateDomainObject(Command,"UpdateProduct");
        domainModel.CreateDomainObject(Event,"ProductUpdated");
        domainModel.CreateDomainObject(Event,"UpdateProductRejected");
        domainModel.SetRelationTargetInstance(Command,"UpdateProduct",publishFact,Event,"ProductUpdated, UpdateProductRejected");

        domainModel.CreateDomainObject(Command,"DeleteProduct");
        domainModel.CreateDomainObject(Event,"ProductDeleted");
        domainModel.CreateDomainObject(Event,"DeleteProductRejected");
        domainModel.SetRelationTargetInstance(Command,"DeleteProduct",publishFact,Event,"ProductDeleted, DeleteProductRejected");

        domainModel.CreateDomainObject(Event,"OrderCreated");
        domainModel.CreateDomainObject(Command,"ReserveProduct");
        domainModel.CreateDomainObject(Event,"ProductReserved");
        domainModel.CreateDomainObject(Event,"ReserveProductRejected");
        domainModel.SetRelationTargetInstance(Event,"OrderCreated",callForAction,Command,"ReserveProduct");
        domainModel.SetRelationTargetInstance(Command,"ReserveProduct",publishFact,Event,"ProductReserved, ReserveProductRejected");

        domainModel.CreateDomainObject(Event,"OrderCanceled");
        domainModel.CreateDomainObject(Command,"ReleaseProduct");
        domainModel.CreateDomainObject(Event,"ProductReleased");
        domainModel.CreateDomainObject(Event,"ReleaseProductRejected");
        domainModel.SetRelationTargetInstance(Event,"OrderCanceled",callForAction,Command,"ReleaseProduct");
        domainModel.SetRelationTargetInstance(Command,"ReleaseProduct",publishFact,Event,"ProductReleased, ReleaseProductRejected");

        domainModel.CreateDomainObject(Event,"OrderApproved");
        domainModel.CreateDomainObject(Command,"DecreaseProductQuantity");
        domainModel.CreateDomainObject(Event,"ProductQuantityDecreased");
        domainModel.CreateDomainObject(Event,"DecreaseProductQuantityRejected");
        domainModel.SetRelationTargetInstance(Event,"OrderApproved",callForAction,Command,"DecreaseProductQuantity");
        domainModel.SetRelationTargetInstance(Command,"DecreaseProductQuantity",publishFact,Event,"ProductQuantityDecreased, DecreaseProductQuantityRejected");

        domainModel.CreateDomainObject(DomainConcept,"Product");
        domainModel.SetRelationTargetInstance(DomainConcept,"Product",isResponsibleFor,Command,"CreateProduct, UpdateProduct, DeleteProduct");
        domainModel.SetRelationTargetInstance(DomainConcept,"Product",isInterestedIn,Event,"OrderCreated, OrderCanceled, OrderApproved");

        await _domainModelRepository.UpdateDomainModelAsync(domainModel);
    }
}
