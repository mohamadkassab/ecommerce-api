using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Collections;
using ecommerce_dash_api.Enum;
using Microsoft.AspNetCore.Http;
using ecommerce_dash_api;
using ecommerce_dash_api.Utils;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class FileTypeValidationAttribute : ValidationAttribute
{
    private static readonly string[] ImageMimeTypes = { "image/" };
    private static readonly string[] VideoMimeTypes = { "video/" };

    private readonly FileTypeEnum _fileTypeEnum;

    public FileTypeValidationAttribute(FileTypeEnum fileTypeEnum)
    {
        _fileTypeEnum = fileTypeEnum;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // Handle single file validation
        if (value is IFormFile file)
        {
            return ValidateFile(file);
        }

        // Handle multiple files (IEnumerable, e.g., list or array)
        if (value is IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                if (item is IFormFile fileInList)
                {
                    var result = ValidateFile(fileInList);
                    if (result != ValidationResult.Success)
                    {
                        return result; // Return the validation error for the first invalid file
                    }
                }
            }
        }

        return ValidationResult.Success;
    }

    private ValidationResult ValidateFile(IFormFile file)
    {
        // Set allowed extensions and MIME types based on the selected FileType
        string[] allowedExtensions;
        string[] allowedMimeTypes;

        switch (_fileTypeEnum)
        {
            case FileTypeEnum.Image:
                allowedExtensions = Constants.ImageExtensions;
                allowedMimeTypes = ImageMimeTypes;
                break;

            case FileTypeEnum.Video:
                allowedExtensions = Constants.VideoExtensions;
                allowedMimeTypes = VideoMimeTypes;
                break;

            case FileTypeEnum.ImageVideo:
                allowedExtensions = Constants.ImageExtensions.Concat(Constants.VideoExtensions).ToArray();
                allowedMimeTypes = ImageMimeTypes.Concat(VideoMimeTypes).ToArray();
                break;

            default:
                return new ValidationResult("Invalid file type.");
        }

        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(extension))
        {
            return new ValidationResult($"Invalid file extension. Allowed extensions are: {string.Join(", ", allowedExtensions)}");
        }

        if (!allowedMimeTypes.Any(mime => file.ContentType.StartsWith(mime)))
        {
            return new ValidationResult($"Invalid MIME type. Allowed types are: {string.Join(", ", allowedMimeTypes)}");
        }

        return ValidationResult.Success;
    }
}
