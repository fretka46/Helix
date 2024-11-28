import type { ServerWebSocket } from "bun";

let users = new Map<string, ServerWebSocket<{ username: string }>>();

const server = Bun.serve<{ username: string }>({
  port: 8000,
  fetch(req, server) {
    const url = new URL(req.url);
    console.log(`upgrade!`);
    const username = req.headers.get("x-username");
    if (!username) {
      return new Response("Username header is required", { status: 400 });
    }
    if(server.upgrade(req, { data: { username } })) {
      return;
    }
    return new Response("WebSocket upgrade error", { status: 400 });
  },
  websocket: {
    open(ws) {
      if (!ws.data.username) {
        ws.close(1008, "Username is required");
        throw new Error("Username is required");
      }
      users.set(ws.data.username, ws);
      const msg = `${ws.data.username} has entered the chat`;
      ws.subscribe("the-group-chat");
      server.publish("the-group-chat", msg);
    },
    message(ws, message) {
      // this is a group chat
      // so the server re-broadcasts incoming message to everyone
      server.publish("the-group-chat", `${ws.data.username}: ${message}`);
    },
    close(ws) {
      users.delete(ws.data.username);
      const msg = `${ws.data.username} has left the chat`;
      ws.unsubscribe("the-group-chat");
      server.publish("the-group-chat", msg);
    },
  },
});

console.log(`Listening on ${server.hostname}:${server.port}`);