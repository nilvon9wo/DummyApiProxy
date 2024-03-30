import React from 'react';
import { describe, expect, it } from 'vitest';
import { render } from '@testing-library/react';
import { screen } from '@testing-library/react';
import { beInTheDocument, haveTextContent } from '@testing-library/jest-dom';
import UserItem from '../../src/features/users/UserItem';

describe('UserItem component', () => { // Renamed description for clarity
  it('renders user name correctly', () => {
    const user = { firstName: 'John', lastName: 'Doe' };
    render(<UserItem user={user} />);

    const nameElement = screen.getByText(/John Doe/i);
    expect(nameElement).toBeInTheDocument();
    expect(nameElement).toBeInstanceOf(HTMLLIElement); // Use toBeInstanceOf matcher
  });
});