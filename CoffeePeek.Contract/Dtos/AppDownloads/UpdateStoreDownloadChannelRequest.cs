#nullable enable

using System.ComponentModel.DataAnnotations;

namespace CoffeePeek.Contract.Dtos.AppDownloads;

public record UpdateStoreDownloadChannelRequest(
    [property: MaxLength(2048)] string? Url,
    bool Enabled);
