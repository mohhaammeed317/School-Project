using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Localization;
using schoolproject.core.resources;

namespace schoolproject.core.bases
{

    public class ResponseHandler
    {
          private readonly IStringLocalizer<SharedResources> _localizer;
        public ResponseHandler(IStringLocalizer<SharedResources> localizer)
        {
          this._localizer = localizer;
        }
        public Response<T> Deleted<T>(string s)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = _localizer[SharedResourcesKey.Deleted]
            };
        }
        public Response<T> Success<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = _localizer[SharedResourcesKey.Success],
                Meta = Meta
            };
        }
        public Response<T> Unauthorized<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = _localizer[SharedResourcesKey.UnAuthorized]
            };
        }
        public Response<T> BadRequest<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? _localizer[SharedResourcesKey.BadRequest] : Message
            };
        }
        
        public Response<T> UnprocessableEntity<T>(string Message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = Message == null ? _localizer[SharedResourcesKey.UnprocessableEntity] : Message
            };
        }

        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? _localizer[SharedResourcesKey.NotFound] : message
            };
        }

        public Response<T> Created<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = _localizer[SharedResourcesKey.Created],
                Meta = Meta
            };
        }
        public Response<T> updated<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = _localizer[SharedResourcesKey.Updated],
                Meta = Meta
            };
        }
    }
}
/* public class ResponseHandler
    {
       
        public ResponseHandler()
        {
           
        }
        public Response<T> Deleted<T>()
        {
            return new Response<T>() {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = "Deleted Successfully"
            };
        }
        public Response<T> Success<T>(T entity, object Meta = null)
        {
            return new Response<T>() {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = "Added Successfully",
                Meta = Meta
            };
        }
        public Response<T> Unauthorized<T>()
        {
            return new Response<T>() {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                Message = "UnAuthorized"
            };
        }
        public Response<T> BadRequest<T>(string Message = null)
        {
            return new Response<T>() {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? "Bad Request" : Message
            };
        }

        public Response<T> NotFound<T>(string message = null)
        {
            return new Response<T>() {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = message == null ? "Not Found" : message
            };
        }
        
        public Response<T> Created<T>(T entity, object Meta = null)
        {
            return new Response<T>() {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
                Message = "created",
                Meta = Meta
            };
        }
    }

*/
