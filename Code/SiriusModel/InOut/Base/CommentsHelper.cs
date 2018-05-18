namespace SiriusModel.InOut.Base
{
    public static class CommentsHelper
    {
        public static string OnDeserialize(string toDeserialize)
        {
            return toDeserialize.Replace("\r\n", "\0").Replace("\n", "\r\n").Replace("\0", "\r\n");
        }
    }
}
