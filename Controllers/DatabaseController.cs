using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FilesUploadApi.Models;
using FilesUploadApi.Database;
using Newtonsoft.Json;
using DBreeze;

namespace FilesUploadApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {
        [HttpPut]
        public JsonResult Exists (DbInfo data)
        {
            if (database.Exists(data.TableName, data.JsonKey))
            {
                return new JsonResult(true);
            }
            return new JsonResult(false);
        }
        [HttpPost]
        public JsonResult CreateUpdate (DbInfo data)
        {
            try
            {
                if (!string.IsNullOrEmpty(data.TableName) && !string.IsNullOrEmpty(data.JsonValue))
                {
                    if (data.JsonKey == "")
                    {
                        string key = Guid.NewGuid().ToString();
                        data.JsonKey = JsonConvert.SerializeObject(key);
                    }
                    if (database.Create(data.TableName, data.JsonKey, data.JsonValue))
                    {
                        return new JsonResult("Data Added successfully!");
                    }
                    else
                    {
                        return new JsonResult("Operation Failed. Ensure your parse data using JSON.parse(data)");
                    }
                }
                else
                {
                    return new JsonResult("TableName and / or JsonValue cannot be null or empty.");
                }
            }
            catch (System.Exception e)
            {
                return new JsonResult(e.Message);
            }
        }
        [HttpPost]
        [Route("read")]
        public JsonResult Read(DbInfo info)
        {
            var data = database.Read(info.TableName, info.JsonKey);
            if (data.Key != "")
            {
                return new JsonResult(data);
            }
            else
            {
                return new JsonResult("data not found, ensure the key passed in is serialized with JSON.Parse(key)");
            }
        }
        [HttpGet("{tablename}")]
        public JsonResult ReadAll(string TableName)
        {
            var data = database.ReadAll(TableName);
            if (data.Count > 0)
            {
                return new JsonResult(data);
            }
            else
            {
                return new JsonResult("data not found in the Tablename passed and / or table is empty, ensure correct data is passed");
            }
        }
        [HttpDelete("{data}")]
        public JsonResult DeleteAll (string data)
        {
            if (database.DeleteAll(data,true))
            {
                return new JsonResult("Delete All operation is successful");
            }
            else
            {
                return new JsonResult("Delete All operation failed.");
            }
        }
        [HttpPut]
        [Route("delete")]
        public JsonResult Delete (DbInfo data)
        {
            if (database.Delete(data.TableName, data.JsonKey))
            {
                return new JsonResult("Delete operation is successful");
            }
            else
            {
                return new JsonResult("Delete operation failed.");
            }
        }
    }
}