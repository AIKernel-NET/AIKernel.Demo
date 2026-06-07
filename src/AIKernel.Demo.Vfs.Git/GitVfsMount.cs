namespace AIKernel.Demo.Vfs.Git;

public sealed record GitVfsMount(
    string RepositoryPath,
    string Branch,
    string RomRoot);
