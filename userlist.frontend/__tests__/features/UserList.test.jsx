import React from 'react';
import { describe, expect, it, beforeEach, afterEach} from 'vitest';
import { render, screen } from '@testing-library/react';
import { beInTheDocument, haveTextContent } from '@testing-library/jest-dom';
import { Response } from 'node-fetch';
import nock from 'nock';
import UserList from '../../src/features/users/UserList';

describe('UserList component', () => {
  beforeEach(() => {
    nock('http://localhost:7278') 
      .get('/api/v1/users')
      .reply(200, { users: [{ id: 1, firstName: 'John', lastName: 'Doe' }] });
  });

  afterEach(() => {
    nock.cleanAll(); 
  });

  it('renders user list with header and no error when data is fetched successfully', async () => {
   // Arrange
    const mockUsers = [{ id: 1, firstName: 'John', lastName: 'Doe' }];
    global.fetch = vi.fn(() => 
        Promise.resolve(new Response(JSON.stringify({ users: mockUsers })))
    );

    // Act
    render(<UserList />);
    await new Promise((resolve) => setTimeout(resolve, 0)); // Wait for async fetch


    // Assert
    const headerElement = screen.getByText('User List');
    const errorAlert = screen.queryByText(/Error/i);
    const userList = screen.getByRole('list');

    expect(headerElement).toBeInTheDocument();
    expect(errorAlert).toBeNull();
    expect(userList).toBeInTheDocument();

    const userItems = screen.getAllByRole('listitem');
    expect(userItems.length).toBe(mockUsers.length);

    expect(userItems[0]).toHaveTextContent(/John Doe/i);
  });
});