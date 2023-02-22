import React, {FC, useEffect, useState} from 'react';
import {useGetMessages, useMessageWebSocket} from './api';
import {Message} from '../../types/message';
import {Group, MantineProvider, Paper} from '@mantine/core';
import {API_URL} from "../../config";
import {ContactList} from "../menu/ContactList";
import Cookies from "js-cookie";
import ky from "ky";
import {AccountClaims} from "../../types/accountClaims";
import './ChatroomForm.css'

export const ChatroomForm: FC = () => {
    const [messages, setMessages] = useState<Message[]>([]);
    const [userId, setUserId] = useState("");
    const getMessages = useGetMessages();

    const lastMessage = useMessageWebSocket();

    async function getUserId() {
        const token = Cookies.get('auth_token');
        const authorizedKy = ky.extend({
            headers: {
                authorization: `Bearer ${token}`
            }
        });

        const response = await authorizedKy.get(`${API_URL}/account`).json<AccountClaims>();

        setUserId(response['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid']);
    }

    async function fetchData() {
        const receivedMessages = await getMessages;
        setMessages(receivedMessages);

        console.log(receivedMessages);
    }

    useEffect(() => {
        getUserId();
        fetchData();
    }, [])

    // useEffect(() => {
    //     fetchData().then()
    // }, [lastMessage])

    // useEffect(() => {
    //     const currentUrl = window.location.href;
    //     const roomId = currentUrl.split('/').pop();
    //     const socket = io(API_URL + '/chatRoom/' + {roomId} + '/message'); // adres hosta socket.io
    //
    //     socket.on('newMessage', (message: Message) => {
    //         setMessages([...messages, message]);
    //         console.log(message);
    //     });
    //
    //     return () => {
    //         socket.disconnect();
    //     };
    // }, [messages]);

    return (
        <MantineProvider theme={{colorScheme: 'dark'}}>
            <Group style={{width: '100%', display: 'flex', height: '100%'}}>
                <Paper shadow="sm" radius="md" p="lg" withBorder style={{flex: 1, height: '100%'}}>
                    <ContactList/>
                </Paper>

                <Paper shadow="sm" radius="md" p="lg" withBorder
                       style={{flex: 4, height: '100%'}}>
                    {messages.map((message) => {

                        const messageClass = message.accountId === userId ? "my-message" : "not-my-message";
                        const classes = `${messageClass} message-entry`;

                        return (
                            <Paper
                                key={message.id} shadow="sm" radius="md" p="lg" withBorder className={classes}>
                                <div>{message.SentDate}</div>
                                <div>{message.textContent}</div>
                            </Paper>
                        )
                    })}
                </Paper>
            </Group>

        </MantineProvider>
    );
};
