
using Microsoft.AspNetCore.Mvc;

namespace ITDesk.SignInResultNameSpace;
public class CheckResultService : ControllerBase
{
    public IActionResult PasswordResult(Microsoft.AspNetCore.Identity.SignInResult result, DateTimeOffset? lockOutEnd)
    {
        if (result.IsLockedOut)
        {
            TimeSpan? timeSpan = lockOutEnd - DateTime.UtcNow;
            if (timeSpan is not null)

                return BadRequest(new
                {
                    Message = $"Your user has been locked for {Math.Ceiling(timeSpan.Value.TotalMinutes)} minutes because you entered your password three times."
                });
            else
            {
                return BadRequest(new
                {
                    Message = $"Your user has been locked for 15 minutes because you entered your password three times."
                });
            }
        }
        if (result.IsNotAllowed)
        {
            return BadRequest(new { Message = "Your mail adddress didn't confirm." });
        }
        if (!result.Succeeded)
        {
            return BadRequest(new { Message = "Your password is false!" });
        }
        return Ok();
    }
}
