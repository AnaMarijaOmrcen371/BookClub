import { render, screen, fireEvent, waitFor } from '@testing-library/react'
import { describe, it, expect, vi } from 'vitest'
import CreateDiscussionForm from '../CreateDiscussionForm'
import * as service from '../../../services/discussionService'

vi.mock('../../../services/discussionService')

describe('CreateDiscussionForm', () => {
  it('shows validation errors when fields are empty', async () => {
    render(<CreateDiscussionForm />)

    fireEvent.click(screen.getByRole('button', { name: /finish/i }))

    expect(await screen.findByText(/title is required/i))
      .toBeInTheDocument()
  })

  it('calls createDiscussion and onCreated on success', async () => {
    const onCreated = vi.fn()

    service.createDiscussion.mockResolvedValueOnce({
      id: 1,
      title: 'My title',
      description: 'My desc'
    })

    render(<CreateDiscussionForm onCreated={onCreated} />)

    fireEvent.change(screen.getByLabelText(/title/i), {
      target: { value: 'My title' }
    })

    fireEvent.change(screen.getByLabelText(/description/i), {
      target: { value: 'My desc' }
    })

    fireEvent.click(screen.getByRole('button', { name: /finish/i }))

    await waitFor(() => {
      expect(service.createDiscussion).toHaveBeenCalled()
      expect(onCreated).toHaveBeenCalledWith(
        expect.objectContaining({ id: 1 })
      )
    })

    expect(
      screen.getByText(/discussion created successfully/i)
    ).toBeInTheDocument()
  })
})
