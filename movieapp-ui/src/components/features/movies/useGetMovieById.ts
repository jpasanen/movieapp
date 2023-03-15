import { FullMovieDto } from './types';
import { apiGet } from '@services/api/apiService';
import { useQuery } from 'react-query';

async function getMovieByIdAsync(id: number): Promise<FullMovieDto> {
  return await apiGet<any, FullMovieDto>(`/${id}`);
}

export function useGetCountryDetails(id: number) {
  return useQuery(['moviedetails', id], () => getMovieByIdAsync(id));
}
