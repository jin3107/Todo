import type { AppResponse } from "../helpers";
import type { SearchRequest, SearchResponse, TaskRequest, TaskResponse } from "../interfaces";
import axios from '../configs/axios'

export const createTask = async (data: TaskRequest) : Promise<AppResponse<TaskResponse>> => {
  const response = await axios.post<AppResponse<TaskResponse>>("/tasks", data);
  return response.data;
}

export const updateTask = async (data: TaskRequest) : Promise<AppResponse<TaskResponse>> => {
  const response = await axios.put<AppResponse<TaskResponse>>("/tasks", data);
  return response.data;
}

export const getTaskById= async (id: string) : Promise<AppResponse<TaskResponse>> => {
  const response = await axios.get<AppResponse<TaskResponse>>(`/tasks/${id}`);
  return response.data;
}

export const deleteTask = async (id: string) : Promise<AppResponse<string>> => {
  const response = await axios.delete<AppResponse<string>>(`tasks/${id}`);
  return response.data;
}

export const searchTasks = async (searchRequest: SearchRequest) : Promise<AppResponse<SearchResponse<TaskResponse>>> => {
  const response = await axios.post<AppResponse<SearchResponse<TaskResponse>>>("/tasks/search", searchRequest);
  return response.data;
};