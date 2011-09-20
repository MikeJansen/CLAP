﻿using System;
using System.IO;
using System.Reflection;

namespace CLAP.Validation
{
    /// <summary>
    /// File exists validation
    /// </summary>
    [Serializable]
    public sealed class FileExistsAttribute : ValidationAttribute
    {
        public FileExistsAttribute()
            : base(new FileExistsValidator())
        {

        }

        public override string Description
        {
            get
            {
                return "File exists";
            }
        }

        private class FileExistsValidator : IParameterValidator
        {
            public void Validate(ParameterInfo parameter, object value)
            {
                string path = string.Empty;

                if (value is Uri)
                {
                    path = ((Uri)value).LocalPath;
                }
                else
                {
                    path = Environment.ExpandEnvironmentVariables((string)value);
                }

                if (!File.Exists(path))
                {
                    throw new ValidationException("{0}: '{1}' is not an existing file".FormatWith(
                        parameter.Name,
                        path));
                }
            }
        }
    }
}