using System;

namespace IPMS.Services.Identity.API.Infrastructure.File
{
    public enum OutputFileFormat
    {
        PDF = 0,
        DOCX,
        XLSX,
        DOC,
        XLS,
        TIFF,
        JPG,
        EMF,
        MSP = 100,
        PRIMAVERA = 101
    }

    public class FileHelper
    {
        public static string GetMime(OutputFileFormat fileFormat)
        {
            switch (fileFormat)
            {
                case OutputFileFormat.DOC:
                    return "application/doc";
                case OutputFileFormat.DOCX:
                    return "application/msword";
                case OutputFileFormat.PDF:
                    return "application/pdf";
                case OutputFileFormat.XLS:
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; //TODO
                case OutputFileFormat.XLSX:
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case OutputFileFormat.TIFF:
                    return "image/tiff";
                case OutputFileFormat.JPG:
                    return "image/jpeg";
                case OutputFileFormat.EMF:
                    return "application/emf";

                case OutputFileFormat.MSP:
                        return "application/vnd.ms-project";
                case OutputFileFormat.PRIMAVERA:
                        return "application/octet-stream";
                default:
                    return null;
            }
        }

        public static string GetExtention(OutputFileFormat fileFormat)
        {
            switch (fileFormat)
            {
                case OutputFileFormat.DOC:
                    return "doc";
                case OutputFileFormat.DOCX:
                    return "docx";
                case OutputFileFormat.PDF:
                    return "pdf";
                case OutputFileFormat.XLS:
                    return "xls";
                case OutputFileFormat.XLSX:
                    return "xlsx";
                case OutputFileFormat.TIFF:
                    return "tiff";
                case OutputFileFormat.JPG:
                    return "jpg";
                case OutputFileFormat.EMF:
                    return "emf";

                case OutputFileFormat.MSP:
                    return "MPP";
                case OutputFileFormat.PRIMAVERA:
                    return "XER";
                default:
                    return "";
            }
        }
    }
}
