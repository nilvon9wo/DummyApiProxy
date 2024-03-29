# UserList Demo Project

This project demonstrates how to set up a simple user interface to view user profiles from dummyapi.io. It uses a React front-end with Vite for bundling and a .NET 8.0 C# isolated Azure Function as a proxy to interact with the dummyapi.io API.

## Prerequisites

To run this project, ensure you have the following installed:

- Windows 10
- .NET SDK 8.0.202
- npm version 9.8.1
- Node.js version 16.17.0

This project has been tested under the above conditions. It might work under different environments, but compatibility is not guaranteed.

## Setup Instructions

1. **Clone the repository**

   Clone this repository to your local machine using your favorite Git client or the following command:

   ```bash
   git clone https://github.com/nilvon9wo/DummyApiProxy.git
   ```

1. **Configure API Key**

Navigate to the root directory of the project and create a file named appsettings.local.json with the following content:

   ```json
   {
      "DummyApiSettings": {
        "ApiKey": "SOME_VALID_API_KEY"
      }
    }
   ```

Replace SOME_VALID_API_KEY with your valid API key from dummyapi.io.

2. **Start the Frontend**

Open a terminal, navigate to the userlist.frontend directory, and run:

   ```bash
    npm install
    npm run dev
   ```

This will start the Vite+React development server.

3. **Start the Backend**
3. **Start the Backend**

Open the DummyApiProxy project in Visual Studio 2022 (v17.9.5 or later) and start the debugging session. This will run the .NET isolated function that acts as a proxy.

## Usage
* The Swagger/OpenAPI documentation for the API can be accessed at http://localhost:7278/api/swagger/ui.
* The Vite+React frontend can be viewed at http://127.0.0.1:5173/.

## Contributing
Feel free to fork the project and submit pull requests for any improvements or fixes.

## License
This project is open-sourced under the MIT License. See the LICENSE file for more details.
