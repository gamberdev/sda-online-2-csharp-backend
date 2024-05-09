namespace ecommerce.utils;

/// Utility class for common functions.
public static class Function
{
    /// Generates a slug based on the provided name.
    public static string GetSlug(string name)
    {
        // Trim leading and trailing spaces, convert to lowercase, and replace spaces with hyphens
        return name.Trim().ToLower().Replace(" ", "-");
    }
}
