namespace todo_service_refwebsoftware.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}