namespace Vulns.Jobs.Base;
public interface IJobCheck<T> where T : Core.Entity
{
    public bool IsCheckPassed();
    public Task<bool> IsCheckPassedAsync(CancellationToken token);
    public bool IsCheckFailing() => !IsCheckPassed();
    public async Task<bool> IsCheckFailingAsync(CancellationToken token) => !await IsCheckPassedAsync(token);
}