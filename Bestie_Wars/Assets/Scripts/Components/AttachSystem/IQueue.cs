public interface IQueue
{
    bool IsCanBeAttach { get; }
    void Attach(IQueuing attachObject);
    void AttachToBehindWithoutRotation(IQueuing attachObject);
    void Detach(IQueuing detachObject);
}