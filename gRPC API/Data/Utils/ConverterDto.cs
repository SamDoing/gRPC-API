namespace gRPC_API.Models.Utils
{
    public static class ConverterDto
    {
        public static TOutput Converter<TOutput, TInput>(in TInput input)
        {
            var output = (TOutput?)Activator.CreateInstance(typeof(TOutput));
            ConvertInPlace(input, output);
            return output;
        }

        public static void ConvertInPlace<TOutput, TInput>(in TInput input, TOutput output, bool checkNull = false)
        {
            foreach (var pI in typeof(TInput).GetProperties())
            {
                foreach (var pO in typeof(TOutput).GetProperties())
                {
                    if (checkNull && pI.GetValue(input) == null) 
                    {
                        break;
                    }
                    
                    if (pI.Name == pO.Name && pI.PropertyType == pO.PropertyType)
                    {
                        pO.SetValue(output, pI.GetValue(input));
                        break;
                    }
                }
            }
        }
    }
}
