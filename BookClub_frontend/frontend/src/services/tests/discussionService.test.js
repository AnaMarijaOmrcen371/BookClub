import { describe, it, expect, vi, beforeEach } from 'vitest'
import { createDiscussion } from '../discussionService'

describe('discussionService', () => {
  beforeEach(() => {
    global.fetch = vi.fn()
  })

  it('sends POST request and returns discussion dto', async () => {
    const mockResponse = {
      id: 1,
      title: 'Test title',
      description: 'Test description'
    }

    fetch.mockResolvedValueOnce({
      ok: true,
      json: async () => mockResponse
    })

    const result = await createDiscussion({
      title: 'Test title',
      description: 'Test description'
    })

    expect(fetch).toHaveBeenCalledWith(
      'https://localhost:7022/api/discussions',
      expect.objectContaining({
        method: 'POST',
        headers: { 'Content-Type': 'application/json' }
      })
    )

    expect(result).toEqual(mockResponse)
  })

  it('throws error when response is not ok', async () => {
    fetch.mockResolvedValueOnce({
      ok: false,
      text: async () => 'Bad request'
    })

    await expect(
      createDiscussion({ title: 'x', description: 'y' })
    ).rejects.toThrow('Bad request')
  })
})
