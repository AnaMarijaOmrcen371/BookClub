import { render, screen, fireEvent, waitFor } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { describe, it, expect, vi } from 'vitest'
import CreateDiscussionPage from '../CreateDiscussionPage'

const mockNavigate = vi.fn()

vi.mock('react-router-dom', async () => {
  const actual = await vi.importActual('react-router-dom')
  return {
    ...actual,
    useNavigate: () => mockNavigate
  }
})

describe('CreateDiscussionPage integration', () => {
  it('creates discussion and navigates to details page', async () => {
    render(
      <MemoryRouter>
        <CreateDiscussionPage />
      </MemoryRouter>
    )

    fireEvent.change(screen.getByLabelText(/title/i), {
      target: { value: 'Integration title' }
    })

    fireEvent.change(screen.getByLabelText(/description/i), {
      target: { value: 'Integration desc' }
    })

    fireEvent.click(screen.getByRole('button', { name: /finish/i }))

    await waitFor(() => {
      expect(mockNavigate).toHaveBeenCalledWith('/discussions/123')
    })
  })
})
