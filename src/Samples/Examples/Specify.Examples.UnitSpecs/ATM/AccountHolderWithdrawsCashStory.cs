using Specify.Stories;

namespace Specify.Examples.UnitSpecs.ATM
{
    public class AccountHolderWithdrawsCashStory : UserStory
    {
        public AccountHolderWithdrawsCashStory()
        {
            AsA = "As an Account Holder";
            IWant = "I want to withdraw cash from an ATM";
            SoThat = "So that I can get money when the bank is closed";
            //ImageUri = "https://upload.wikimedia.org/wikipedia/commons/d/d3/49024-SOS-ATM.JPG";
            //StoryUri = "http://google.com";
        }
    }
}