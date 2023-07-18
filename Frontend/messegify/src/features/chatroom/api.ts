import Cookies from "js-cookie";
import ky from "ky";
import {API_URL} from "../../config";
import {Message} from "../../types/message";
import {useState} from "react";
import useWebSocket from "react-use-websocket";
import {AccountClaims} from "../../types/accountClaims";

export function useGetMessages() {
    const currentUrl = window.location.href;
    const roomId = currentUrl.split('/').pop();
    const endpoint = new URL('/board/all', API_URL).href

    const token = Cookies.get('auth_token');
    const authorizedKy = ky.extend({
        headers: {
            authorization: `Bearer ${token}`
        }
    });

    return authorizedKy.get(`${API_URL}/chatRoom/${roomId}/message/list`).json<Message[]>();
}

export function useMessageWebSocket() {
    const currentUrl = window.location.href;
    const roomId = currentUrl.split('/').pop();
    const apiUri = process.env.REACT_APP_API_URL;
    const hostname = `localhost:5000`;
    const endpoint = `wss://${API_URL}`;
    const [socketUrl,] = useState(endpoint);

    const {
        sendMessage,
        lastMessage,
        readyState,
        getWebSocket,
        lastJsonMessage,
        sendJsonMessage
    } = useWebSocket(socketUrl);

    return {
        sendMessage,
        lastMessage,
        readyState,
        getWebSocket,
        lastJsonMessage,
        sendJsonMessage
    }
}

export async function handleSubmit(data: Message, roomId: string) {
    try {
        console.log("data = " + data.textContent);

        const token = Cookies.get('auth_token');
        const authorizedKy = ky.extend({
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                authorization: `Bearer ${token}`
            },
            body: JSON.stringify({
                textContent: data.textContent
            })
        });

        return authorizedKy.post(`${API_URL}/chatRoom/${roomId}/message`).json<Message[]>();
    } catch (error) {

    }
}

export async function getUserId() {
    const token = Cookies.get('auth_token');
    const authorizedKy = ky.extend({
        headers: {
            authorization: `Bearer ${token}`,
        },
    });

    const response = await authorizedKy.get(`${API_URL}/account/me`).json<AccountClaims>();
    const userId = response['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid'];

    return userId;
}