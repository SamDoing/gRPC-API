using Grpc.Net.Client;
using gRPC_API.Data;
using gRPC_API.Data.Repositorys;
using gRPC_API_Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static gRPC_API_Test.ProductService;

namespace gRPC.Tests
{
    public class ProductAPIIntegration
    {
        private readonly GrpcChannel channel;
        private readonly  ProductServiceClient client;
        private readonly ShopDbContext context;

        public ProductAPIIntegration()
        {
            channel = GrpcChannel.ForAddress("http://localhost:5160");
            client = new(channel);
            
            var servico = new ServiceCollection();

            var connectionString = "Server=127.0.0.1,1433; Database=Shop;User=SA;Password=Pa$$w00rd;Trusted_Connection=False";
            servico.AddDbContext<ShopDbContext>(opt => opt.UseSqlServer(connectionString));

            context = servico.BuildServiceProvider().GetService<ShopDbContext>();

            context.Database.EnsureCreated();

            Console.WriteLine($"Connect ? {context.Database.CanConnect()}");
        }

        [Fact(DisplayName = "Should connect to DB")]
        public void ConnectionTesting()
        {
            Assert.True(context.Database.CanConnect());
        }

        //[Fact(DisplayName = "Should create, edit, get and delete a product in DB")]
        //public void ProductIntegrationTesting()
        //{
        //    var resp = client.CreateProduct(new() { Name = "Prod", Category = "Noa", Description = "Porsch" });
        //    Assert.True( resp.Name == "Prod" );
        //    Assert.True( resp.Category == "Noa");
        //    Assert.True( resp.Description == "Porsch");


        //    client.EditProduct(new() 
        //    {
        //        Id = resp.Id,
        //        Product = new() {
        //            Name = "Pascal",
        //            Category = "ABC",
        //            Description = "Cassandra"
        //        } 
        //    });

        //    var resp2 = client.GetProduct(new() { Id = resp.Id });
        //    Assert.True(resp2.Name == "Pascal");
        //    Assert.True(resp2.Category == "ABC");
        //    Assert.True(resp2.Description == "Cassandra");

        //    Assert.NotNull( client.DeleteProduct(new() { Id = resp2.Id }) );
        //}
    }
}