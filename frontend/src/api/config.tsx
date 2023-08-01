export const BACKEND_BASE_URL: URL = new URL(import.meta.env.VITE_BACKEND_URL)
export const BACKEND_API_URL: URL = new URL('/api/vlist', BACKEND_BASE_URL)
export const BACKEND_HEALTH_URL: URL = new URL('/health', BACKEND_BASE_URL)