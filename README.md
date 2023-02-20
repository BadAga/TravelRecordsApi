# TravelRecordsApi
This API provides a comprehensive system for web applications that integrates **Entity Framework Core** for the data layer, **JSON Web Token** (JWT) authorization for authentication, **Azure Blob Storage** for data storage, **Azure SQL Database** for cloud storage, and a robust **MVC** design pattern for the application layer. In addition, this API implements secure password hashing for all user accounts. This README will provide an overview of the API and its components.

## Database and Storage
The API is based on Entity Framework Core and uses a SQL Database hosted on Azure. The API uses Azure Blob Storage for file storage and retrieval. Only photos were stored in the container. In order to match the photos to specific posts, user profile photos, or trip main picture the following naming convention was used: A_B_C_D.jpg, where A is the user's id, B is the id of the trip, C is the id of the stage, and D is the id of the post, a string of characters in A_B_C_D was the id of the photo.

## Web Application premises
The API was created for a web application that streamlines the preparation of travel reports, a convenient and intuitive system for organizing travel data. It allows you to view past travel histories and create new ones. Users can log in or register. Each user can create trips, each trip consists of stages, and a stage consists of posts (photo and photo description) and a list of attractions. In addition to each trip, a map of stages is created (in the form of pins on the map). The user can edit his data.

## Getting Started
Here's a step-by-step guide to setting up and using TravelRecordsApi

  - Make sure your database is compatible with table model (see Models directory in repo)
  - Change connection string in appsettings.json
    ![Zrzut ekranu_20230220_113630](https://user-images.githubusercontent.com/72341763/220087324-81038bbb-0d9d-4c2a-82ba-2f553650b958.png)
  - Change Blob Storage connection sring and container name in appsettings.json
    ![Zrzut ekranu_20230220_120435](https://user-images.githubusercontent.com/72341763/220088058-a61a5e6d-5776-4140-b15f-e30b1fe20d6b.png)
  - In order to authorize requests in Swagger UI click Authorize button in top right corner and follow the Bearer scheme (Bearer {token})
  - If you'll ever come across CORS error try uncommenting cors section in Program.cs

  
## API Endpoints 
Table of all created endpoints and expected responses.

| Category | HTTP Verbs | Endpoint      | Possible response codes  | action  |
| -------- |----------- |-------------- | -------------------- | -------------------  |
| Login    | POST       | /api/Login  | 200 | JWT token|  
| Users    | GET       | /api/Users | 200, 401, 403  | returns registered users |  
| Users    | GET       | /api/Users/{id} | 200, 401, 403, 404 | returns user with set id |  
| Users    | GET       | /api/Users/{username}/{password} | 200, 401, 403, 404 | returns user with set username and password |  
| Users    | POST | /api/Users | 201, 409 | creates new user|  
| Users    | PUT       | /api/Users/{id}  | 204, 400, 401, 404, 409| updates user values with set id|  
| Users    | DELETE       | /api/Users/{id} | 204, 401, 403, 404|deletes user with set id|  
| Trips | GET |/api/Trips | 200, 401, 403 | returns all trips|
| Trips | GET |/api/Trips/{id} | 200, 401, 403, 404 | returns trip with set id |
| Trips | GET |/api/Trips/{userId}/userTrips | 200, 401, 403, 404 | returns users's trips with set user id |
| Trips | POST |/api/Trips | 201, 401, 403, 404, 409 | creates new trip |
| Trips | PUT |/api/Trips/{id} | 204, 400, 401, 404 | updates trip values with set id|
| Trips | DELETE |/api/Trips/{id} | 204, 401, 403, 404 | deletes trip with set id |  
| Stages | GET |/api/Stages | 200, 401, 403 | returns all stages|
| Stages | GET |/api/Stages/{id} | 200, 401, 403, 404 | returns stages with set id |
| Stages | GET |/api/Stages/{tripId}/tripStages | 200, 401, 403, 404 | returns trips's stages with set trip id |
| Stages | POST |/api/Stages | 201, 401, 403, 404, 409 | creates new stage |
| Stages | PUT |/api/Stages/{id} | 204, 400, 401, 404 | updates stage values with set id|
| Stages | DELETE |/api/Stages/{id} | 204, 401, 403, 404 | deletes stage with set id | 
| Posts  | GET |/api/Posts | 200, 401, 403 | returns all posts|
| Posts | GET |/api/Posts/{id} | 200, 401, 403, 404 | returns posts with set id |
| Posts | GET |/api/Posts/{tripId}/stagePosts | 200, 401, 403, 404 | returns stage's posts with set stage id |
| Posts | GET |/api/Posts/{tripId}/tripPosts | 200, 401, 403, 404 | returns trips's posts with set trip id |
| Posts | POST |/api/Posts | 201, 401, 403, 404, 409 | creates new post |
| Posts | PUT |/api/Posts/{id} | 204, 400, 401, 404 | updates post values with set id|
| Posts | DELETE |/api/Posts/{id} | 204, 401, 403, 404 | deletes post with set id | 
| Attractions  | GET |/api/Attractions | 200, 401, 403 | returns all attarctions|
| Attractions | GET |/api/Attractions/popularAttractions | 200, 401, 403, 404 | returns attarctions with popularity set to HIGH |
| Attractions | GET |/api/Attractions/{attractionId} | 200, 401, 403, 404 | returns attractions with set id |
| Attractions | GET |/api/Attractions/{stageId}/allStageAttractions | 200, 401, 403, 404 |returns all attractions connected to stage with set id |
| Attractions | POST |/api/Attractions | 200, 401, 403 | creates new attraction |
| Attractions | POST |/api/Attractions/{attractionId}/{stageId} | 200, 400, 401, 403| creates new connection between attraction and stage at set ids |
| Attractions | PUT |/api/Attractions/{attractionId} | 204, 400, 401, 500 | updates attraction values with set id|
| Attractions | DELETE |/api/Attractions/{attractionId} | 204, 401, 403, 404 | deletes attraction with set id | 
| Attractions | DELETE |/api/Attractions/{attractionId}/{stageId}| 204, 400, 401, 403, 404 | deletes connection between attraction and stage at set ids | 
|Storage | GET | /api/Storage | 200 | Get all files at the Azure Storage Location and return them |
|Storage | GET | /api/Storage/{userId}/{travelId}/{stageId}/{postId} | 200, 404 | returns file with set ids |
|Storage | POST | /api/Storage/{imageId} | 200, 500 | uploads file, and renames it to fit image id |
|Storage | POST | /api/Storage/{userId}/{travelId}/{stageId}/{postId}| 200, 409, 500 |  uploads file, and renames it to fit image id (combination of passed ids) |
|Storage| DELETE| /api/Storage/{userId}/{travelId}/{stageId}/{postId} | 200, 404, 500 | deletes file with set image id (combination of passed ids) |



