namespace DotNet.Testcontainers.Tests.Fixtures
{
  using System.Threading.Tasks;
  using DotNet.Testcontainers.Builders;
  using DotNet.Testcontainers.Configurations;
  using DotNet.Testcontainers.Containers;
  using StackExchange.Redis;

  public sealed class RedisFixture : DatabaseFixture<RedisTestcontainer, IConnectionMultiplexer>
  {
    private readonly TestcontainerDatabaseConfiguration configuration = new RedisTestcontainerConfiguration();

    public RedisFixture()
    {
      this.Container = new TestcontainersBuilder<RedisTestcontainer>()
        .WithDatabase(this.configuration)
        .Build();
    }

    public override async Task InitializeAsync()
    {
      await this.Container.StartAsync()
        .ConfigureAwait(false);

      this.Connection = ConnectionMultiplexer.Connect(this.Container.ConnectionString);
    }

    public override async Task DisposeAsync()
    {
      this.Connection.Dispose();

      await this.Container.DisposeAsync()
        .ConfigureAwait(false);
    }

    public override void Dispose()
    {
      this.configuration.Dispose();
    }
  }
}
