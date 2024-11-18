"use client";

import storage from 'redux-persist/lib/storage';

const createNoopStorage = () => {
    return {
        getItem: () => Promise.resolve(null),
        setItem: () => Promise.resolve(),
        removeItem: () => Promise.resolve(),
    };
};

const storageEngine = typeof window !== 'undefined' ? storage : createNoopStorage();

export default storageEngine;