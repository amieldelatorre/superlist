import { BACKEND_API_URL } from './config';

export type AcceptedCreateListFormData = {
  createdBy: string;
  title: string;
  description: string;
  passphrase: string;
  expiry: string;
};


export async function sendApiRequest(method: string, data: AcceptedCreateListFormData): Promise<any> {
  const response = await fetch(BACKEND_API_URL, {
    method: method,
    cache: "no-cache",
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json"
    },
    redirect: "follow",
    referrerPolicy: "no-referrer",
    body: JSON.stringify(data),
  });

  return response;
}