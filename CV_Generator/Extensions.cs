using System.Text;

namespace CV_Generator
{
    internal static class Extensions
    {
        /// <summary>
        /// Append \n (instead of \r\n)
        /// </summary>
        /// <param name="builder"></param>
        public static StringBuilder AppendNewLine(this StringBuilder builder)
        {
            builder.Append("\n");
            return builder;
        }

        /// <summary>
        /// Append text and \n (instead of \r\n)
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        public static StringBuilder AppendNewLine(this StringBuilder builder, string value)
        {
            builder.Append(value);
            builder.Append("\n");
            return builder;
        }
    }
}
