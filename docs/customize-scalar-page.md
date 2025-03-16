# Customizing Scalar Page
I mean why would you? This is basically playing around with fun styling of the API Documentation page.

![scalar-view](/assets/setting-up/setup-seven.png)

## Prerequisites
Before getting started, let's assume you have done the following:

- Completed the step before this. [Configure Scalar For ASP.NET Core Web API](/docs/add-scalar-to-web-api.md)
- Contemplated if this is for you (I have).

## The Breakdown
A nice way to navigate with ease.

- [Setting Page Title for Scalar](#setting-options-for-scalar)
- [Setting Theme for Scalar](#setting-theme-for-scalar)
- [Change Layout for Scalar](#change-layout-for-scalar)


## Setting Page Title for Scalar
Let's do some cool browser tab changes.

#### **Page title**
```csharp
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // Edit this here 
}
```

#### **Before**

![title-view](/assets/setting-up/setup-ten.png)


#### **After**

```csharp
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => 
    {
        options.Title = "Luke's Scalar API"; // Add title like this
    });
}
```

![title-view](/assets/setting-up/setup-eleven.png)

**Things to note:**

You will see there was two options for title. `WithTitle` ,Basically two ways for the same thing , ones more fluently done, the other is just a property and a value as we using it.

## Setting Theme for Scalar
Not one for light-mode(flash-bang)?


In order to change the theme add the following:

```csharp
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => 
    {
        options.Title = "Luke's Scalar API";
        options.Theme = ScalarTheme.DeepSpace; // Added theme
    });
}
```

So pretty !!!!

![layout-view](/assets/setting-up/setup-twelve.png)

## Change Layout for Scalar
By default we got the modern view, then we got the classic that we can choose from.

```csharp
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => 
    {
        options.Title = "Luke's Scalar API";
        options.Theme = ScalarTheme.DeepSpace;
        options.Layout = ScalarLayout.Classic; // Added Classic Layout
    });
}
```

Not too shabby! You can change it back to Modern for the other one.

![theme-view](/assets/setting-up/setup-thirteen.png)

Maybe you dont want that button in the corner for the modern theme where it says **"Open API Client"**

```csharp
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => 
    {
        options.Title = "Luke's Scalar API";
        options.Theme = ScalarTheme.DeepSpace;
        options.Layout = ScalarLayout.Modern;
        options.HideClientButton = true; // Hide the button
    });
}
```
Swimming with the fishes!

![theme-view](/assets/setting-up/setup-fourteen.png)

## Next Steps
Too much code in your `Program.cs`, See how to clean it up and seperate concerns.

[Clean up Program.cs](/docs/clean-up-program-cs.md)