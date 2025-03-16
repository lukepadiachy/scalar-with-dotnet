# Configure Scalar For ASP.NET Core Web API
We'll look at how you can configure your ASP.NET Web API to make use of Scalar

## Prerequisites
Before getting started, let's assume you have the following:
- .NET 9 SDK installed
- Visual Studio
- A basic understanding of middleware and dependency injection in ASP.NET Core

## The Breakdown
A nice way to navigate with ease.

- [Create A ASP.NET Core Web API Project](#create-a-aspnet-core-web-api-project)
- [Install Scalar](#install-scalar)

## Create A ASP.NET Core Web API Project
Let's get the basics out the way by creating out project.

### **Select the template**

![template-image](/assets/setting-up/setup-one.png)

### **Give it a name**

![project-name](/assets/setting-up/setup-two.png)

### **Select the following**

![project-specs](/assets/setting-up/setup-three.png)

Enabling OpenAPI support in your ASP.NET Core Web API project on .NET 9 allows your application to automatically generate standardized documentation for its HTTP APIs. This documentation outlines available endpoints, request and response formats and other essential details, making it easier for developers to understand and interact with your API.

**Key Features of OpenAPI Support in .NET 9:**

- **Automatic Documentation Generation:** OpenAPI specifications are created at runtime, reflecting the current state of your API endpoints.

- **Integration with Tools:** OpenAPI documentation integrates seamlessly with tools like the one we'll be connecting in a few, providing interactive display for exploring and testing API endpoints.

- [**API-ly Ever After: OpenAPI in .NET 9 - Youtube**](https://www.youtube.com/watch?v=pkQdwbYPRP4)

### Let's do some tweaks for now

- Delete this file and clean up comments in `Program.cs`

![project-cleanup](/assets/setting-up/setup-four.png)


## **Install Scalar**
There's two ways you can do this:

### **Command Line Interface**

```bash
dotnet add package Scalar.AspNetCore --version 2.0.36
``` 
### **Nuget Package Manager**

- Right-click on `Dependencies`
- Click on `Manage Nuget Packages`
- Make sure you're in the Browse Tab
- Search `Scalar` & `Install`

![project-specs](/assets/setting-up/setup-five.png)

Then you should be good to go.

## **Connect In Code**

### **`Program.cs`**

In order to make use of **Scalar** we need to do the following

```csharp
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // Add this here 
}
```
It will automatically pick up what you are trying to use and add `using Scalar.AspNetCore;` at the top of your `Program.cs`

This is the default set up of it.

At this point you should see **"This localhost page can't be found"** since this is an api project its normal.

so now we do this in the url:

#### **Before**

```markdown
https://localhost:7003/
```

Our ports do not have to match

#### **After**

This is where you want to be

```markdown
https://localhost:7003/weatherforecast
```

It would look something like this

![endpoint-view](/assets/setting-up/setup-six.png)

This is where Scalar does some magic.

#### **Using Scalar in URL**
Let's do this

```markdown
https://localhost:7003/scalar/v1
```

![scalar-view](/assets/setting-up/setup-seven.png)

- `scalar` is the API **resource**.  
- `v1` indicates **version 1** of the API, helping manage updates without breaking existing functionality.

Here's the `weatherforecast` in Pretty Pretty mode.

![scalar-view](/assets/setting-up/setup-eight.png)

Testing the end-point

![scalar-view](/assets/setting-up/setup-nine.png)

## Success !!!
Woohoo you have basically added Scalar to your ASP.NET Core Web API.

## Next Steps

[Customizing Scalar Page](/docs/customize-scalar-page.md)
