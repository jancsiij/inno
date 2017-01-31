
using System.Text;


namespace Inno01.Helpers
{
    public static class TimeHelper
    {
        public static string ConvertToHM(int intervale)
        {
            if (intervale != 0)
            {
                int hours = intervale / 60;
                int minutes = intervale - (hours * 60);

                var sb = new StringBuilder();

                if (hours != 0)
                {
                    sb.Append(hours + " óra ");
                }
                if (minutes != 0)
                {
                    sb.Append(minutes + " perc ");
                }

                return sb.ToString();
            }

            return string.Empty;
        }
    }
}
