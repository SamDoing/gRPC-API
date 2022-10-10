using Grpc.Net.Client;
using gRPC_API_Test;
using static gRPC_API_Test.ProductService;

namespace gRPC.Tests
{
    public class ProductAPIIntegration
    {
        private readonly GrpcChannel channel;
        private readonly  ProductServiceClient client;

        public ProductAPIIntegration()
        {
            channel = GrpcChannel.ForAddress("http://localhost:5160");
            client = new(channel);
        }

        [Fact(DisplayName = "Should create, edit, get and delete a product in DB")]
        public void ProductIntegrationTesting()
        {
            var resp = client.CreateProduct(new() { Name = "Prod", Category = "Noa", Description = "Porsch" });
            Assert.True( resp.Name == "Prod" );
            Assert.True( resp.Category == "Noa");
            Assert.True( resp.Description == "Porsch");


            client.EditProduct(new() 
            {
                Id = resp.Id,
                Product = new() {
                    Name = "Pascal",
                    Category = "ABC",
                    Description = "Cassandra"
                } 
            });

            var resp2 = client.GetProduct(new() { Id = resp.Id });
            Assert.True(resp2.Name == "Pascal");
            Assert.True(resp2.Category == "ABC");
            Assert.True(resp2.Description == "Cassandra");

            Assert.NotNull( client.DeleteProduct(new() { Id = resp2.Id }) );
        }
    }
}