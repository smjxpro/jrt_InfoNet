export interface APIResponse<T> {
  success: boolean;
  message: string | null;
  data: T | null;
}
