#nullable enable

using System.ComponentModel.DataAnnotations;

namespace CoffeePeek.Contract.Dtos.AppDownloads;

public record CreateAndroidReleaseRequest(
    [property: Required]
    [property: MaxLength(32)]
    string Version,
    int VersionCode,
    [property: Required]
    [property: MaxLength(2048)]
    string FileUrl,
    [property: Required]
    [property: MaxLength(255)]
    string FileName,
    long FileSize,
    [property: Required]
    [property: StringLength(64, MinimumLength = 64)]
    string Sha256,
    DateTime? ReleasedAt);
