﻿using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace Utils.Transformers
{
    public class UrlTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value == null)
            {
                return null;
            }

            return Regex
                .Replace(
                    value.ToString()!,
                    "([a-z])([A-Z])",
                    "$1-$2",
                    RegexOptions.CultureInvariant,
                    TimeSpan.FromMilliseconds(100)
                )
                .ToLowerInvariant();
        }
    }
}
