using Bullseye;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace build
{
    internal static class CommandLineApplicationBullseyeExtensions
    {
        public static void AddBullseyeOptions(this CommandLineApplication app, Func<OptionDefinition, bool> filter = null)
        {
            app.Argument("targets", "The targets to run or list.", true);
            foreach (var option in Options.Definitions)
            {
                if (filter?.Invoke(option) ?? true)
                    app.Option((option.ShortName != null ? $"{option.ShortName}|" : "") + option.LongName, option.Description, CommandOptionType.NoValue);
            }
        }

        public static IEnumerable<string> GetBullseyeTargets(this CommandLineApplication app)
        {
            var arg = app.Arguments.SingleOrDefault(arg => arg.Name == "targets");
            return arg?.Values ?? Enumerable.Empty<string>();
        }

        public static Options GetBullseyeOptions(this CommandLineApplication app)
        {
            var values = new List<(string name, bool value)>();
            foreach (var d in Options.Definitions)
            {
                var opt = app.Options.SingleOrDefault(o => "--" + o.LongName == d.LongName);
                if (opt != null)
                {
                    values.Add((d.LongName, opt.HasValue()));
                }
            }

            return new Options(values);
        }
    }
}
