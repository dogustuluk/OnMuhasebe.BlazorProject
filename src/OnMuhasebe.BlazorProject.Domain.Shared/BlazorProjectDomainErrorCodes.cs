namespace OnMuhasebe.BlazorProject;

/// <summary>
/// business hata kodlarını burada tanımlıyoruz.
/// </summary>
public static class BlazorProjectDomainErrorCodes
{
    //Hatanın açıklamasını henüz yazmadık. İlgili açıklamayı localize işlemi yapıldığı zaman yapılacaktır.
    public const string DuplicateKod = "Exception:00001";
    public const string CannotBeDeleted = "Exception:00002";
    public const string Required = "Exception:00003";
    public const string MaxLenght = "Exception:00004";
    public const string GreaterThanOrEqual = "Exception:00005";
}
