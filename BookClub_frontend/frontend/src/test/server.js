import { setupServer } from 'msw/node'
import { http } from 'msw'

export const server = setupServer(
  http.post('https://localhost:7022/api/discussions', async ({ request }) => {
    const body = await request.json()

    return new Response(
      JSON.stringify({
        id: 123,
        title: body.title,
        description: body.description
      }),
      { status: 200 }
    )
  })
)
