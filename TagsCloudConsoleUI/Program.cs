﻿using Autofac;
using System.Drawing;
using ResultPattern;
using TagsCloudConsoleUI.DIPresetModules;
using TagsCloudGenerator;

namespace TagsCloudConsoleUI
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            ConsoleManager.Run(new DefaultConsoleFormatter(), OnCallAction);
        }

        private static IContainer BuildContainer(BuildOptions options)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new CircularRandomCloudModule(options));
            builder.RegisterModule(new WordParserWithYandexToolModule(options));
            builder.RegisterModule(new BitmapImageCreatorModule(options));

            builder.RegisterInstance(options.InputFileName).Named<string>("filepath");

            return builder.Build();
        }

        private static Result<Bitmap> OnCallAction(BuildOptions options)
        {
            var container = BuildContainer(options);

            var fullPath = options.InputFileName;
            var size = new Size(options.Width,options.Height);
            var format = container.Resolve<CloudFormat>();

            var bitmapImageResult = container.Resolve<CloudBuilder<Bitmap>>()
                .CreateTagCloudRepresentation(fullPath, size, format);

            if(bitmapImageResult.IsSuccess)
                bitmapImageResult
                    .GetValueOrThrow()
                    .Save(options.OutputFileName, ImageFormatter.ParseImageFormat(options.ImageExtension));
            
            return bitmapImageResult;
        }
    }
}
