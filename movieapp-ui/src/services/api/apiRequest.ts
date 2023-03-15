import HttpMethod from './httpMethod';

export default interface ApiRequest<D> {
  method?: HttpMethod;
  resource: string;
  payload?: D;
}