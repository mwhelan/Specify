using Specify.Stories;

namespace Specify.Tests.Stubs
{
    class WithdrawCashUserStory : UserStory
    {
        public WithdrawCashUserStory()
        {
            AsA = "As an Account Holder";
            IWant = "withdraw cash from an ATM";
        }
    }

    class TicTacToeUserStory : UserStory
    {
        public TicTacToeUserStory()
        {
            Title = "Tic Tac Toe Story";
            TitlePrefix = "User Story 1:";
            AsA = "As a player";
            IWant = "I want to have a tic tac toe game";
            SoThat = "So that I can waste some time!";
        }
    }

    class WithdrawCashValueStory : ValueStory
    {
        public WithdrawCashValueStory()
        {
            AsA = "As an Account Holder";
            IWant = "withdraw cash from an ATM";
            InOrderTo = null;
        }
    }

    class TicTacToeValueStory : ValueStory
    {
        public TicTacToeValueStory()
        {
            Title = "Tic Tac Toe Story";
            TitlePrefix = "User Story 1:";
            InOrderTo = "waste some time!";
            AsA = "As a player";
            IWant = "I want to have a tic tac toe game";
        }
    }
}
