import asyncio
import websockets
import sys

async def send_message(uri):
    headers = {
        'x-username': 'fitus'
    }
    async with websockets.connect(uri, additional_headers=headers) as websocket:
        async def send():
            while True:
                message = await asyncio.get_event_loop().run_in_executor(None, sys.stdin.readline)
                message = message.strip()
                if message:
                    await websocket.send(message)
                    #print(f"Sent: {message}")

        async def receive():
            while True:
                response = await websocket.recv()
                print(response)

        send_task = asyncio.create_task(send())
        receive_task = asyncio.create_task(receive())

        await asyncio.wait([send_task, receive_task], return_when=asyncio.FIRST_COMPLETED)

async def main():
    uri = "wss://helix.kvitek.engineer/"
    await send_message(uri)

if __name__ == "__main__":
    try:
        asyncio.run(main())
    except KeyboardInterrupt:
        print("Disconnected from server")