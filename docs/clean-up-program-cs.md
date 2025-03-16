# Clean up Program.cs
I actually enjoyed this when I saw it for the first time, its just so much clean.

## Prerequisites
Before getting started, let's assume you have done the following:

- Completed the step before this. [Customizing Scalar Page](/docs/customize-scalar-page.md)

## The Breakdown
A nice way to navigate with ease.

- [Remove template endpoints](#remove-template-endpoints)
- [Seperation of Concerns](#seperation-of-concerns)
- [Change Layout for Scalar](#change-layout-for-scalar)


## Remove template endpoints
Out with the old!

```csharp
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => 
    {
        options.Title = "Luke's Scalar API";
        options.Theme = ScalarTheme.DeepSpace;
        options.Layout = ScalarLayout.Modern;
        options.HideClientButton = true;
    });
}

app.UseHttpsRedirection();

app.Run();
```

Removed the sample endpoint code, added a small one for now.

```csharp
app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!"); // hehe here we are

app.Run();
```

## Seperation of Concerns
Something about having a structure that references where specific things are just make sense!

### Moving out the API Configuration
This was probably one of the nicest things Ive ever seen done. We going to give this code a place to call home **#extensions**. If you put everything directly in Program.cs, it can get messy and hard to read. Extension methods help clean things up by moving alot of setup code into separate files. This keeps `Program.cs` simple while still allowing our app to work the same way.

Think of it like organizing your room: instead of leaving everything on the floor (messy code in Program.cs), you put similar things into drawers or boxes (extension methods in separate files).

### OpenAPI Services Using an Extension Method
Create a folder in your project configuration and call it **Extensions**

In that folder we going to create a file and call it `OpenApiConfig.cs`. Next well create some methods inside here that we can use.

```csharp
namespace SampleApiScalar.Extensions
{
    public static class OpenApiConfig
    {
      public static void AddOpenApiServices(this IServiceCollection services) 
      { 
            services.AddOpenApi();
      }
    }
}
```
Then in our `Program.cs`

#### **Before:**  
We were directly calling OpenAPI setup like this:  

```csharp
builder.Services.AddOpenApi();
```

#### **After:**  
We changed it to:  

```csharp
using SampleApiScalar.Extensions; // usings

builder.Services.AddOpenApiServices(); // Use our method instead
```


### **What’s Happening Here?**
- We made a **separate class** (`OpenApiConfig`) to handle OpenAPI setup.  
- We created a method (`AddOpenApiServices`) that **registers OpenAPI services**.  
- The `this IServiceCollection services` part lets us call this method **like a built-in feature** of `builder.Services`.

### **Why Make It `static`?**
✅ **We don’t need to create an object** – We can call the method directly.  
✅ **It works as an "add-on"** – Extension methods **must** be in a `static` class.  
✅ **Keeps memory use low** – The method is always available without needing extra setup.

### **Why Is This Better?**
✔ **Cleaner Code** – `Program.cs` is easier to read.  
✔ **Easier to Update** – If we need to change OpenAPI settings, we only update one file.  
✔ **Reusable** – We can use this method in other projects or parts of our app. 

## Next Let's Target out Scalar Config
Since we already got our file for this we can just add another method to it.

```csharp
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => 
    {
        options.Title = "Luke's Scalar API";
        options.Theme = ScalarTheme.DeepSpace;
        options.Layout = ScalarLayout.Modern;
        options.HideClientButton = true;
    });
}
```
Added this method to our `OpenApiConfig.cs`

```csharp
public static void UseOpenApi(this WebApplication app) // explain why we didnt make it nullable here 
{
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options.Title = "Luke's Scalar API";
            options.Theme = ScalarTheme.DeepSpace;
            options.Layout = ScalarLayout.Modern;
            options.HideClientButton = true;
        });
    }
}
```
### **What’s Happening Here?**
- **This method is for the app (not services)** – That’s why it extends `WebApplication` instead of `IServiceCollection`.  
- **It only runs in development mode** – We check `if (app.Environment.IsDevelopment())` to **only enable OpenAPI locally** (not in production).  
- **It sets up the API docs** – `MapOpenApi()` adds OpenAPI, and `MapScalarApiReference()` customizes the Scalar API docs.

---

### **Updating `Program.cs`**

Before, we had all the OpenAPI setup inside `Program.cs`:

```csharp
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Luke's Scalar API";
        options.Theme = ScalarTheme.DeepSpace;
        options.Layout = ScalarLayout.Modern;
        options.HideClientButton = true;
    });
}
```

Now, we **just call our new helper method**:

```csharp
app.UseOpenApi();
```

This makes `Program.cs` **much cleaner** and easier to read.

---

### **Why Didn’t We Make `app` Nullable?**

The method starts like this:  

```csharp
public static void UseOpenApi(this WebApplication app)
```

- **We don’t make `app` nullable (`WebApplication? app`) because**:
  - The `WebApplication` **must** exist when this method runs.
  - If `app` were `null`, the app wouldn't be able to start at all.
  - We always call `UseOpenApi()` after `var app = builder.Build();`, so `app` is **guaranteed** to exist.

---

### Why Is This Better?

✅ **Keeps `Program.cs` clean** – No more long blocks of OpenAPI setup.  
✅ **Easier to manage** – If we need to change OpenAPI settings, we only update `OpenApiConfig`.  
✅ **More reusable** – The method can be used in other projects without copying code.


That summary is 🔥! Here’s an even more relatable version:  

### Summary  
Imagine your kitchen is a mess, and every time you cook, you have to dig through drawers for the same pots and spices. Annoying, right?  

Now, you get smart and **organize everything into labeled containers**. You put all the spices in one place and all the pots in another. Now when you cook, instead of searching for things, you just grab what you need.  

That’s exactly what we did here. We **moved all the OpenAPI setup into one organized file**, so `Program.cs` isn’t a mess anymore. Now, instead of repeating a bunch of setup code, we just **call two simple methods** and everything works. So much cleaner. Still gives me the chills when I look at it. 

### Round 2: FIGHT !!!!
Since we did all that, Lets do the same for our **dependencies**. Create a file called `DependenciesConfig`

#### **Before:**  
In `Program.cs`, we directly called `builder.Services.AddOpenApiServices();` which makes use of our new method. This worked, but we can take it a step further too.

#### **After:**  
Now, we moved that setup into the `AddDependencies()` method. So, instead of calling `AddOpenApiServices()` in `Program.cs`, we just call `builder.AddDependencies();`.

```csharp
namespace SampleApiScalar.Extensions
{
    public static class DependenciesConfig
    {
        public static void AddDependencies(this WebApplicationBuilder builder) 
        {
            builder.Services.AddOpenApiServices();
        }
    }
}
```


### **Why This Makes Sense**

- **Cleaner Code** – By grouping everything related to dependencies in `AddDependencies()`, we keep `Program.cs` much easier to read and maintain.  
- **Easier to Update** – If we want to add more dependencies later (like adding a new service), we only need to change `DependenciesConfig.cs`, not `Program.cs`.  
- **Flexibility** – It makes it easier to **swap** out different services or configurations later without looking through multiple files.

### **Builder vs IServiceCollection: What's the Difference?**

**`IServiceCollection`**  
   - Think of it as the **list** where you add things your app needs (like services or helpers).
   - It's a **basic tool** for registering services so your app can use them later (for example, a service that fetches data from a database).
   - You mostly use it when you want to tell your app about things it needs to run.

**`WebApplicationBuilder`**  
   - This is like a **helper tool** that combines the list (`IServiceCollection`) with other setup tasks for your app.
   - It’s used during the app setup phase to organize everything, including registering services, setting up routes, security and more.

### **Why Use `WebApplicationBuilder` Instead of `IServiceCollection`?**

Here’s what happens when we use `WebApplicationBuilder`:

```csharp
public static void AddDependencies(this WebApplicationBuilder builder) 
{
    builder.Services.AddOpenApiServices();
}
```

- **Convenience** – `WebApplicationBuilder` makes life easier because it lets us **combine multiple setup tasks** in one place (like adding services and setting up routes or security). This way, everything is organized and we don’t have to go back and forth between different parts of the app setup.
  
- **Access to Everything** – `WebApplicationBuilder` already has a **`Services`** property, which lets us use `IServiceCollection` easily. So, when we call `builder.Services.AddOpenApiServices();`, we’re using `IServiceCollection` but inside the bigger setup tool.

### **Why Not Just Use `IServiceCollection`?**

- **Limited Use** – `IServiceCollection` is only for adding services. It doesn’t help with other important setup tasks like routing (how requests are handled) or security.
  
- **Not Flexible Enough** – `WebApplicationBuilder` is like an **all-in-one** tool that handles both services and other app setup tasks. It's more flexible and keeps everything organized.

---

### **Summary:**

- **`WebApplicationBuilder`** is the **main tool** for setting up your app, and it can handle both services and the app setup (like routes and security).
- **`IServiceCollection`** is just for adding services to the app, but it doesn’t handle other setup tasks.
- Using **`WebApplicationBuilder`** lets us keep everything **clean, organized, and in one place**, making it easier to manage the app's setup. 

In short: `WebApplicationBuilder` is like the **master controller** for your app, while `IServiceCollection` is just the list for your services.

### Round 3: You thought we were done ?!
Next let's tackle the endpoint!

**Create a Folder for Endpoints:**
- First, let's create a folder called **`Endpoints`** in the project. This will be the place where we group all of our endpoint configurations.

**Create `RootEndpoints.cs`:**
- Inside the `Endpoints` folder, create a new file named **`RootEndpoints.cs`**. This is where we will add our root endpoint logic.

**Move the Root Endpoint:**
- In `Program.cs`, we have the root endpoint like this:
```csharp
app.MapGet("/", () => "Hello World!");
```
We’ll move this to a method inside the `RootEndpoints.cs` file.

**Create the Extension Method:**
- Inside `RootEndpoints.cs`, we’ll create a static class with a static method `AddRootEndpoints(this WebApplication app)`. This method will house the logic for the root endpoint.

```csharp
namespace SampleApiScalar.Endpoints
{
    public static class RootEndpoints
    {
        public static void AddRootEndpoints(this WebApplication app)
        {
            app.MapGet("/", () => "Hello World!");
        }
    }
}
```

**Update `Program.cs`:**
- Now, back in `Program.cs`, we can clean things up by calling our new method:
```csharp
using SampleApiScalar.Endpoints;
using SampleApiScalar.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddDependencies();

var app = builder.Build();

app.UseOpenApi();
app.UseHttpsRedirection();
     
// Use our method to add root endpoints
app.AddRootEndpoints();

app.Run();
```

### **What Happened?**

- **Created `RootEndpoints.cs`:** We moved the logic for the root endpoint into its own file to follow the principle of separation of concerns. Now, everything is more organized.
  
- **Created `AddRootEndpoints` method:** This method encapsulates the logic of the root endpoint and allows us to call it from `Program.cs`, keeping the setup code neat and tidy.

- **Updated `Program.cs`:** Now `Program.cs` is much cleaner! All the configuration is still there, but we’ve offloaded the specific tasks (like adding root endpoints) into separate methods, which makes the app easier to manage and extend.

### **Summary**
Basically you seeing the pattern we following ? We took the root endpoint out of `Program.cs` and placed it into its own dedicated file (`RootEndpoints.cs`). Now, if we need to add more endpoints or tweak the root logic, it’s all in one place, keeping things neat and easy to manage.

## Does it still work?
Let's run this application and see what's up.

The images will reveal all.

![normal-view](/assets/setting-up/setup-fifteen.png)

Ah we forgot something

![scalar-view](/assets/setting-up/setup-sixteen.png)

Wooohoo we still in it to win it!

## Next Steps
Let's look at some resources

[Resources](/docs/resources.md)