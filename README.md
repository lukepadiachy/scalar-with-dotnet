# API Documentation in .NET 9 with Scalar

![scalar-with-dotnet-image](/assets/scalar-with-dotnet-image.png)

## Hmm Introduction

This repository serves as a guide for implementing Scalar in ASP.NET Core Web APIs,as an alternative to Swagger UI documentation. 

It includes:

- **Complete Web API Example**: A functional ASP.NET Core Web API project showcasing Scalar integration
- **Step-by-Step Documentation**: Detailed guides covering installation, configuration, and customization
- **Best Practices**: Examples of code organization and separation of concerns
- **Customization Options**: Various examples of Scalar themes, layouts, and configuration options
- **Practical Examples**: Working endpoints with documentation integration
- **Resource Collection**: Curated list of official documentation, tutorials, and community resources

This repository provides practical examples and detailed guidance for creating beautiful, interactive API documentation with Scalar in .NET 9.

## Understanding API Documentation

When building APIs (Application Programming Interfaces), it's good to provide clear documentation that explains how to use them. 

This documentation basically includes:

- **Endpoints**: The URLs clients can access.
- **HTTP Methods**: Such as GET, POST, PUT, DELETE, indicating the type of operation.
- **Request Parameters**: Data required to make a request.
- **Responses**: Possible outcomes and their data structures.

Proper documentation ensures that developers can effectively integrate and interact with your API.

## You Remember Swagger? 

**Swagger** has been a popular tool for generating interactive API documentation. It allows developers to visualize and test API endpoints through a user-friendly interface. Tools like **Swashbuckle** made integrating Swagger into ASP.NET Core projects straightforward, automating the creation of interactive API docs.

## Changes in .NET 9

With the release of .NET 9, Swagger UI is no longer included by default in ASP.NET Core Web API projects. Which was kinda like "What do we do now?" Well this is the purpose of this repo. There are many alternatives. I saw a lot of videos related to [Scalar](https://guides.scalar.com/scalar/introduction), so I thought lets get into that

## Introducing Scalar

**Scalar** is an open-source API platform designed to document, test and discover APIs. 

It provides:

- **Interactive API Documentation**: Provides a built-in interactive playground that doubles as a full-featured API client.
- **OpenAPI Support**: Seamlessly integrates with your OpenAPI (formerly Swagger) documents.
- **Offline-First API Client**: Allows testing and interacting with APIs without an active internet connection.

For more details, visit the [Scalar website](https://scalar.com/).

## Benefits of Using Scalar

- **Modern Interface**: With Scalar, you can easily tweak the look and behavior of your documentation to match your project's branding, giving you more control over its appearance.
- **Interactive Testing**: Allows developers to test API endpoints directly within the documentation.
- **Seamless Integration**: Easily integrates with existing OpenAPI specifications in ASP.NET Core projects.
- **Open-Source and Community-Driven**: Being open-source, Scalar benefits from continuous improvements and contributions from the developer community, ensuring it stays up-to-date with the latest trends and needs.

## Sections

**[Configure Scalar For ASP.NET Core Web API](/docs/add-scalar-to-web-api.md)** 


**[Customizing Scalar Page](/docs/customize-scalar-page.md)**


**[Clean up Program.cs](/docs/clean-up-program-cs.md)**


**[Resources](/docs/resources.md)**


# THANK TIM CORREY FOR AN AMAZING VIDEO
After watching a video of Tim Correy on this topic. I decided to create docs for myself based on what I've learnt so far. Now I can always come back to re-learn and add more to it as I learn more on this topic

You can find this video [here]((/docs/resources.md)).
