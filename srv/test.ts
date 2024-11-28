Bun.serve({
  port: 8001,
  fetch(req) {
    return new Response("it runs!");
  }
});

console.log(`Listening on 8001`);