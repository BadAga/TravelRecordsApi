# TravelRecordsApi
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



