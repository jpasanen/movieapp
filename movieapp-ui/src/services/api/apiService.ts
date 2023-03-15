import { toast } from 'react-hot-toast';
import ApiRequest from './apiRequest';
import axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from 'axios';
import HttpMethod from './httpMethod';
import ProblemDetails from './problemDetails'
import { ApiUrl } from './apiConfig'

interface IHeaders {
  [header: string]: string;
}

export default async function apiRequest<D, T>({
  method,
  resource,
  payload
}: ApiRequest<D>): Promise<AxiosResponse<T>> {
  if (!method) {
    method = HttpMethod.GET;
  }

  const options: AxiosRequestConfig = {
    method,
    url: `${ApiUrl}${resource}`
  };

  axios.interceptors.response.use(
    (response: AxiosResponse): AxiosResponse => {
      return response;
    },
    (error: AxiosError) => {
      if (error.response) {
        switch (error.response.status) {
          case 404:
            toast.error('404 - Resource not found');
            break;
          case 500: {
            const fallbackMessage = 'Something went wrong while performing this action.';
            try {
              const internalServerError = error.response.data as ProblemDetails;
              toast.error(internalServerError.detail ?? internalServerError.title ?? fallbackMessage);
            } catch {
              toast.error(fallbackMessage);
            }
            break;
          }
          default:
            toast.error('Something went wrong.');
        }
      }
    }
  );

  return axios(options);
}

export async function apiGet<D, T>(resource: string, payload?: D): Promise<T> {
  return apiRequest<D, T>({ resource, payload }).then((result) => result.data);
}