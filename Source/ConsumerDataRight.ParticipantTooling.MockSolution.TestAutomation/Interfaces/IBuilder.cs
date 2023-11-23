namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Interfaces
{
    public interface IBuilder<out T>
    {
        T Build();
    }

    public interface IAsyncBuilder<T>
    {
        Task<T> BuildAsync();
    }

    public interface IBuilder<T, TBuildArg>
    {
        Task<T> Build(TBuildArg buildArg);
    }
}
